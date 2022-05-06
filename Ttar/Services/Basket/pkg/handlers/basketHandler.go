package basketHandler

import (
	. "basket/pkg/model"
	redisService "basket/pkg/services"
	"encoding/json"
	"net/http"

	"github.com/gorilla/mux"
)

func CreateHandler(redisService *redisService.RedisConfig) http.HandlerFunc {
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
			redisService.SetStringValue(basket.UserId, strData)
		}
	}
}

func GetHandler(redisService *redisService.RedisConfig) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		vars := mux.Vars(r)
		id := vars["id"]

		if data, err := redisService.GetStringValue(id); err != nil {
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
