import { getToken } from "./authManager";
const baseUrl = '/api/ingredientReview'


export const getIngredientReviewById = (id) => {
    return fetch(baseUrl + `/${id}`)
        .then((res) => res.json())
}

export const addIngredientReview = (ingredientReview) => {
    return getToken().then((token) => {
        return fetch(baseUrl + `/create`, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(ingredientReview)
        })
    })
}


export const deleteIngredientReview = (id) => {
    return fetch(baseUrl + `/delete/${id}`, {
        method: "DELETE",
    })
}

export const editIngredientReview = (id, IngredientReview) => {
    return fetch(baseUrl + `/${id}`, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(IngredientReview)
    })

}