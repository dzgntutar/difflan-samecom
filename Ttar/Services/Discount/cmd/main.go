package main

import (
	discountHandler "discount/pkg/handlers"
	"fmt"
	"log"
	"net/http"

	"github.com/gorilla/mux"
)

func main() {
	router := mux.NewRouter()

	router.HandleFunc("/api/discount", discountHandler.GetAllHandler()).Methods("GET")

	fmt.Printf("Server started at %s\n", ":5013")
	log.Fatal(http.ListenAndServe(":5013", router))
}
