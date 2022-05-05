package main

import (
	handlers_p "basket/pkg/handler"
	redis_p "basket/pkg/redis"
	"fmt"
	"log"
	"net/http"

	"github.com/gorilla/mux"
)

func main() {

	config := redis_p.NewRedisClient("redisdb:6379", "", 0)

	valueRedisPing := config.Ping()

	fmt.Println(valueRedisPing)

	router := mux.NewRouter()

	router.HandleFunc("/api/baskettest", handlers_p.TestHandler()).Methods("GET")
	router.HandleFunc("/api/basket/{id:[0-9]+}", handlers_p.GetHandler(config)).Methods("GET")
	router.HandleFunc("/api/basket", handlers_p.CreateHandler(config)).Methods("POST")

	fmt.Printf("Server started at %s\n", ":5012")
	log.Fatal(http.ListenAndServe(":5012", router))

}
