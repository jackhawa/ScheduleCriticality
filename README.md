docker tag schedulecriticality_api jackhawa/schedulecriticality_api
docker push jackhawa/schedulecriticality_api

docker tag schedulecriticality_sqlinux jackhawa/schedulecriticality_sqlinux
docker push jackhawa/schedulecriticality_sqlinux

docker tag schedulecriticality_web jackhawa/schedulecriticality_web
docker push jackhawa/schedulecriticality_web

Run sqlinux: docker run -p 1433:1433 schedulecriticality_sqlinux

Stop all containers
docker stop $(docker ps -a -q)

Remove all containers
docker rm $(docker ps -a -q)

Remove all images
docker rmi $(docker images -a -q) -f

Install docker or docker-machine and run following command:

Run docker quick start terminal with piveleges

docker-machine create --driver virtualbox --virtualbox-memory 4096 default

eval $(docker-machine env default)

docker-machine start

close bash and restart it.

docker pull image

netsh interface portproxy add v4tov4 listenaddress=127.0.0.1 listenport=5000 connectaddress=192.168.99.100 connectport=5000
netsh interface portproxy add v4tov4 listenaddress=127.0.0.1 listenport=4200 connectaddress=192.168.99.100 connectport=4200