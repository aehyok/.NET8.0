
#停止容器
docker stop $(docker ps -a -q --filter "name=sun")

# 移除容器
docker rm $(docker ps -a -q --filter "name=sun")

# 删除本地旧镜像
images=$(docker images --format "{{.ID}} {{.Repository}}" | grep sun)

# 将镜像 ID 和名称放入数组中
IFS=$'\n' read -rd '' -a image_array <<<"$images"

# 遍历数组并删除所有旧的镜像
for ((i=1; i<${#image_array[@]}; i++))
do
    image=${image_array[$i]}
    image_id=${image%% *}
    docker rmi $image_id
done


docker build -t sun-basic -f Dockerfile-Basic .

docker run --restart always -itd --name sun-basic -p 11001:11001 sun-basic

docker build -t sun-systemservice -f Dockerfile-SystemService .

docker run --restart always -itd --name sun-systemservice -p 11000:11000 sun-systemservice

docker build -t sun-ncdp -f Dockerfile-NCDP .

docker run --restart always -itd --name sun-ncdp -p 11002:11002 sun-ncdp