package main

import (
	. "basket/pkg/handler"
	myredis "basket/pkg/redis"
	"fmt"
	"log"
	"net/http"
)

func main() {

	config := myredis.NewRedisClient("", "", 0)

	fmt.Println(config)

	mux := http.NewServeMux()
	mux.Handle("/basket", BasketHandler(config))

	fmt.Println("Serving on port 5012")

	err := http.ListenAndServe(":5012", mux)
	log.Fatal(err)

}
