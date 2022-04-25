package route

import (
	"fmt"
	"log"
	"net/http"

	"github.com/go-redis/redis"
)

func BasketRoute(w http.ResponseWriter, r *http.Request) {
	switch r.Method {
	case "GET":
		getHandler(w, r)
	case "POST":
		postHandler(w, r)
	}
}

func getHandler(w http.ResponseWriter, r *http.Request) {

	fmt.Fprintf(w, "Get\n")

	client := redis.NewClient(&redis.Options{Addr: "", Password: "", DB: 10})

	pong, err := client.Ping().Result()

	if err != nil {
		log.Fatal(err)
	}

	fmt.Fprintf(w, pong)
}

func postHandler(w http.ResponseWriter, r *http.Request) {

	fmt.Fprintf(w, "Post\n")
}
