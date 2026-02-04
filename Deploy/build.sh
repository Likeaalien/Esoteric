#!/bin/bash

sudo docker rm -f esoteric
sudo docker build --no-cache -t esoteric-webgl .
sudo docker run -d --name esoteric -p 10000:80 --restart unless-stopped esoteric-webgl
