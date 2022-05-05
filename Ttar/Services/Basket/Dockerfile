FROM golang:alpine

RUN apk update && apk add --no-cache git && apk add --no-cache bash && apk add build-base

RUN mkdir /app
WORKDIR /app

COPY Services/Basket/go.mod Services/Basket/go.sum .

RUN go get -d -v ./...
RUN go install -v ./...
RUN go mod tidy

COPY Services/Basket .

WORKDIR /app/cmd

RUN go build -o /build

# EXPOSE 5012

CMD [ "/build" ]


