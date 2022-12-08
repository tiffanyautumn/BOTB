import { getToken } from "./authManager";
const baseUrl = '/api/productReview'

export const getProductReviewsByProductId = (id) => {
    return fetch(baseUrl + `/api/product/brand/${id}`)
        .then((res) => res.json())
}

export const addProductReview = (productReview) => {
    return getToken().then((token) => {
        return fetch(baseUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(productReview)
        })
    })
}


export const deleteUserProduct = (id) => {
    return fetch(baseUrl + `/delete/userProduct/${id}`, {
        method: "DELETE",
    })
}