package main

import (
	"basket/pkg/route"
	"net/http"
)

func main() {
	http.HandleFunc("/", route.CreateBasket)

	http.ListenAndServe(":5012", nil)
}
