FROM golang:alpine

RUN apk update && apk add --no-cache git && apk add --no-cache bash && apk add build-base

RUN mkdir /app
WORKDIR /app

COPY Services/Discount/go.mod Services/Discount/go.sum ./

RUN go get -d -v ./...
RUN go install -v ./...
RUN go mod tidy

COPY Services/Discount .

WORKDIR /app/cmd

RUN go build -o /build

CMD [ "/build" ]


