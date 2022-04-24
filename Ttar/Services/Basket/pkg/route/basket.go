package route

import (
	"fmt"
	"net/http"
)

func CreateBasket(w http.ResponseWriter, r *http.Request) {
	switch r.Method {
	case "GET":
		getHandler(w, r)
	case "POST":
		postHandler(w, r)
	}
}

func getHandler(w http.ResponseWriter, r *http.Request) {

	fmt.Fprintf(w, "Get\n")
}

func postHandler(w http.ResponseWriter, r *http.Request) {

	fmt.Fprintf(w, "Post\n")
}
