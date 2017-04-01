docker tag ang2:1.1 jackhawa/schedule-criticality:1.1
docker push jackhawa/schedule-criticality:1.1

Remove all containers
docker rm $(docker ps -a -q)

Remove all images
docker rmi $(docker images -a -q) -f