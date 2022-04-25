package main

import (
	. "basket/pkg/route"
	"net/http"
)

func main() {
	http.HandleFunc("/", BasketRoute)

	http.ListenAndServe(":5012", nil)
}
