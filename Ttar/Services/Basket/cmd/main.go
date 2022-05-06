package main

import (
	basketHandler "basket/pkg/handlers"
	redisService "basket/pkg/services"
	"fmt"
	"log"
	"net/http"

	"github.com/gorilla/mux"
)

func main() {

	config := redisService.NewRedisClient("redisdb:6379", "", 0)

	valueRedisPing := config.Ping()

	fmt.Println(valueRedisPing)

	router := mux.NewRouter()

	router.HandleFunc("/api/baskettest", basketHandler.TestHandler()).Methods("GET")
	router.HandleFunc("/api/basket/{id:[0-9]+}", basketHandler.GetHandler(config)).Methods("GET")
	router.HandleFunc("/api/basket", basketHandler.CreateHandler(config)).Methods("POST")

	fmt.Printf("Server started at %s\n", ":5012")
	log.Fatal(http.ListenAndServe(":5012", router))

}
