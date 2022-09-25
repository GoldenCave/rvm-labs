# pull official base node alpin image https://hub.docker.com/_/node?tab=description&amp%3Bpage=1&amp%3Bname=alpine
FROM node:18-alpine3.15 as builder

# set working directory
WORKDIR /app

# install app dependencies
COPY package.json ./
COPY package-lock.json ./
RUN npm install
COPY . ./

# create .next distribution folder
RUN npm run build

FROM node:18-alpine3.15 as runner
# adapted from https://dev.to/maxdemaio/nextjs-and-docker-2496
# You only need to copy next.config.js if you are NOT using the default configuration
# COPY --from=builder /app/next.config.js ./
COPY --from=builder /app/public ./public
COPY --from=builder /app/.next ./.next
COPY --from=builder /app/node_modules/next ./node_modules/next
COPY --from=builder /app/node_modules/.bin/next ./node_modules/.bin/next
COPY --from=builder /app/package.json ./package.json

ENV PATH /node_modules/.bin:$PATH

CMD ["next", "start"]