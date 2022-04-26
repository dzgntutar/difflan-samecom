package handlers

import (
	"basket/pkg/redis"
	"fmt"
	"net/http"
)

func BasketHandler(redisConfig *redis.RedisConfig) http.Handler {
	fmt.Println(redisConfig)
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		switch r.Method {
		case "GET":
			value := redisConfig.Ping()
			w.Write([]byte("Received a GET request\n"))
			w.Write([]byte(value))

		case "POST":

			w.Write([]byte("Received a POST request\n"))
		default:
			w.WriteHeader(http.StatusNotImplemented)
			w.Write([]byte(http.StatusText(http.StatusNotImplemented)))
			w.WriteHeader(http.StatusOK)
			w.Write([]byte("Get"))
		}
	})

}
