
#停止容器
docker stop $(docker ps -a -q --filter "name=aehyok")

# 移除容器
docker rm $(docker ps -a -q --filter "name=aehyok")

# 删除本地旧镜像
images=$(docker images --format "{{.ID}} {{.Repository}}" | grep aehyok)

# 将镜像 ID 和名称放入数组中
IFS=$'\n' read -rd '' -a image_array <<<"$images"

# 遍历数组并删除所有旧的镜像
for ((i=1; i<${#image_array[@]}; i++))
do
    image=${image_array[$i]}
    image_id=${image%% *}
    docker rmi $image_id
done


docker build -t aehyok-basic -f Dockerfile-Basic .

docker run --restart always -itd --name aehyok-basic -p 11001:8080 aehyok-basic

docker build -t aehyok-systemservice -f Dockerfile-SystemService .

docker run --restart always -itd --name aehyok-systemservice -p 11000:8080 aehyok-systemservice

docker build -t aehyok-ncdp -f Dockerfile-NCDP .

docker run --restart always -itd --name aehyok-ncdp -p 11002:8080 aehyok-ncdp