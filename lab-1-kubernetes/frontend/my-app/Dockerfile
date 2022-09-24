# Adapted from: https://mherman.org/blog/dockerizing-a-react-app/
# Changed from above to use multi stage build for fun (actually saves a lot of space 177MB vs 823MB). 78% space savings.
# pull official base image https://hub.docker.com/_/node?tab=description&amp%3Bpage=1&amp%3Bname=alpine
FROM node:18-alpine3.15 as builder

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY package.json ./
COPY package-lock.json ./
RUN npm install
RUN npm install react-scripts@3.4.1 -g 
COPY . ./
RUN npm run build

FROM node:18-alpine3.15 as runner

COPY --from=builder /app/build ./build
# Deployment tips: https://create-react-app.dev/docs/deployment/
RUN npm install -g serve

# To listen on a different port, override this in your docker run command.
CMD ["serve", "-s", "build", "-l", "3000"]