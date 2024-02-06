FROM node:21-alpine

RUN apk add --no-cache bash

USER node

WORKDIR /usr/src/Client

COPY ./package*.json ./

RUN npm ci

COPY --chown=node:node . .

ENTRYPOINT ["./entrypoint.sh"]
CMD ["sleep", "infinity"]
