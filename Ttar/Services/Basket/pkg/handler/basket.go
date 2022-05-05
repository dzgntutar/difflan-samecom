package handlers_p

import (
	. "basket/pkg/model"
	redis_p "basket/pkg/redis"
	"encoding/json"
	"net/http"

	"github.com/gorilla/mux"
)

func CreateHandler(redisConfig *redis_p.RedisConfig) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		var basket Basket

		decoder := json.NewDecoder(r.Body)
		defer r.Body.Close()

		if err := decoder.Decode(&basket); err != nil {
			w.Header().Set("Content-Type", "application/json")
			w.WriteHeader(http.StatusBadRequest)
			w.Write([]byte("Bad request"))
		} else {
			var totalP float32
			for _, value := range basket.BasketItem {
				totalP += float32(value.Quantity) * value.Price
			}
			basket.TotalPrice = totalP
		}

		if data, err := json.Marshal(basket); err != nil {
			w.Header().Set("Content-Type", "application/json")
			w.WriteHeader(http.StatusBadRequest)
			w.Write([]byte("Bad request"))
		} else {
			strData := string(data)
			redisConfig.SetStringValue(basket.UserId, strData)
		}
	}
}

func GetHandler(redisConfig *redis_p.RedisConfig) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		vars := mux.Vars(r)
		id := vars["id"]

		if data, err := redisConfig.GetStringValue(id); err != nil {
			w.Header().Set("Content-Type", "application/json")
			w.WriteHeader(http.StatusNotFound)
			w.Write([]byte("Basket not found"))
		} else {
			w.Header().Set("Content-Type", "application/json")
			w.WriteHeader(http.StatusOK)
			w.Write([]byte(data))
		}
	}
}

func TestHandler() http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {

		w.Header().Set("Content-Type", "application/json")
		w.WriteHeader(http.StatusOK)
		w.Write([]byte("Success"))

	}
}
