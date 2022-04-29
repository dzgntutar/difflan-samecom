package handlers_p

import (
	. "basket/pkg/model"
	"basket/pkg/redis"
	"encoding/json"
	"fmt"
	"github.com/gorilla/mux"
	"net/http"
	"strconv"
)

func CreateHandler(redisConfig *redis_p.RedisConfig) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		var basket Basket

		decoder := json.NewDecoder(r.Body)

		if err := decoder.Decode(&basket); err != nil {
			return
		}
		defer r.Body.Close()

		var totalP float32
		for _, value := range basket.BasketItem {
			totalP += float32(value.Quantity) * value.Price
		}
		basket.TotalPrice = totalP

		fmt.Println(basket)

		data, _ := json.Marshal(basket)

		strData := string(data)

		redisConfig.SetStringValue(basket.UserId, strData)
	}
}

func GetHandler(redisConfig *redis_p.RedisConfig) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		vars := mux.Vars(r)
		id, err := strconv.Atoi(vars["id"])
		if err != nil {
			fmt.Println(err)
			return
		}
		fmt.Println(id)
		data, _ := redisConfig.GetStringValue("basket")
		fmt.Println(data)
	}
}
