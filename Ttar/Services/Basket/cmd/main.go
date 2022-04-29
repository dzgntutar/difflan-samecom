package main

import (
	handlers_p "basket/pkg/handler"
	redis_p "basket/pkg/redis"
	"fmt"
	"github.com/gorilla/mux"
	"log"
	"net/http"
)

func main() {

	config := redis_p.NewRedisClient("", "", 0)

	router := mux.NewRouter()

	router.HandleFunc("/api/basket/{id:[0-9]+}", handlers_p.GetHandler(config)).Methods("GET")
	router.HandleFunc("/api/basket", handlers_p.CreateHandler(config)).Methods("POST")

	fmt.Println("Serving on port 5012")

	fmt.Printf("Server started at %s\n", ":5012")
	log.Fatal(http.ListenAndServe(":5012", router))

}
