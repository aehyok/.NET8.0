using aehyok.Infrastructure.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Events;
using Xabe.FFmpeg.Downloader;
using Microsoft.IdentityModel.Tokens;
using aehyok.Infrastructure.Utils;

namespace aehyok.Infrastructure.Video
{
    public static class VideoHelper
    {
        /// <summary>
        /// 视频截图
        /// </summary>
        /// <param name="inputPath">视频文件地址</param>
        /// <param name="outputPath">截图输出地址</param>
        /// <param name="captureTime"></param>
        /// <returns></returns>
        public static async Task SnapshotAsync(string inputPath, string outputPath, TimeSpan captureTime)
        {
            await CheckFFmpeg();

            var mediainfo = await FFmpeg.GetMediaInfo(inputPath);

            if (mediainfo.Duration < captureTime)
            {
                captureTime = TimeSpan.FromSeconds(Math.Floor(mediainfo.Duration.TotalSeconds));
            }

            var conversion = await FFmpeg.Conversions.FromSnippet.Snapshot(inputPath, outputPath, captureTime);

            conversion.OnProgress += (object sender, Xabe.FFmpeg.Events.ConversionProgressEventArgs args) =>
            {
                Log.Information($"当前处理进度:{args.Percent}%, 耗时:{args.Duration}");
            };

            var result = await conversion.Start();

            Log.Information($"视频截图处理完成，耗时:{result.Duration},开始时间:{result.StartTime},结束时间:{result.EndTime}");
        }

        /// <summary>
        /// 视频转码（转为 h264 编码的 mp4 格式视频）
        /// </summary>
        /// <param name="inputPath"></param>
        /// <param name="outputPath"></param>
        /// <param name="onProgress"></param>
        /// <returns></returns>
        public static async Task TranscodeAsync(string inputPath, string outputPath, ConversionProgressEventHandler onProgress)
        {
            await CheckFFmpeg();

            var conversion = await FFmpeg.Conversions.FromSnippet.Transcode(inputPath, outputPath, VideoCodec.h264, AudioCodec.copy, Xabe.FFmpeg.Streams.SubtitleStream.SubtitleCodec.copy);

            if (onProgress != null)
            {
                conversion.OnProgress += onProgress;
            }

            await conversion.Start();
        }

        /// <summary>
        /// 判断视频是否为 H264 编码
        /// </summary>
        /// <param name="inputPath"></param>
        /// <returns></returns>
        public static async Task<bool> IsH264Codec(string inputPath)
        {
            await CheckFFmpeg();

            var mediaInfo = await FFmpeg.GetMediaInfo(inputPath);

            foreach (var stream in mediaInfo.VideoStreams)
            {
                if (stream.Codec != Enum.GetName(VideoCodec.h264))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 检查本地是否包含 FFmpeg 库，没有就自动下载
        /// </summary>
        /// <returns></returns>
        public static async Task CheckFFmpeg()
        {
            var commonOptions = App.Options<CommonOptions>();

            var path = GetFFmpegPath();
            Directory.CreateDirectory(path);

            FFmpeg.SetExecutablesPath(path);

            // 判断目录是否包含 ffmpeg 可执行文件
            if (Directory.GetFiles(path).Any(a => Path.GetFileName(a).ToLower().Contains("ffmpeg")))
            {
                return;
            }

            try
            {
                using var downloadProcessFlag = File.Open(Path.Combine(path, "download"), FileMode.OpenOrCreate, FileAccess.Write);

                var process = new Progress<ProgressInfo>();

                process.ProgressChanged += (object sender, ProgressInfo e) =>
                {
                    Log.Information($"当前下载进度:{Math.Round(e.DownloadedBytes * 100d / e.TotalBytes, 2)}% Total Bytes:{e.TotalBytes}, Downloaded Bytes:{e.DownloadedBytes}");
                };

                await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official, FFmpeg.ExecutablesPath, process);
            }
            catch
            {
                Log.Warning($"其他服务正在下载 FFmpeg ，当前服务不再下载");
            }
        }

        public static string GetFFmpegPath()
        {
            var rootPath = Path.Combine(AppContext.BaseDirectory, "libs/ffmpeg");

            var commonOptions = App.Options<CommonOptions>();
            if (!commonOptions.FFmpeg.IsNullOrEmpty()) 
            {
                rootPath = commonOptions.FFmpeg;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                switch (RuntimeInformation.OSArchitecture)
                {
                    case Architecture.X86:
                        return Path.Combine(rootPath, "linux/x86");

                    case Architecture.X64:
                        return Path.Combine(rootPath, "linux/x64");

                    case Architecture.Arm64:
                        return Path.Combine(rootPath, "linux/arm64");
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                switch (RuntimeInformation.OSArchitecture)
                {
                    case Architecture.X86:
                        return Path.Combine(rootPath, "win/x86");

                    case Architecture.X64:
                        return Path.Combine(rootPath, "win/x64");
                }
            }

            throw new InvalidOperationException("暂不支持该类型操作系统");
        }
    }
}
