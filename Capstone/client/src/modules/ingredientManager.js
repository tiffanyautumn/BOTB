const baseUrl = '/api/ingredient'

export const getAllIngredients = () => {
    return fetch(baseUrl)
        .then((res) => res.json())
}

export const getIngredientById = (id) => {
    return fetch(baseUrl + `/${id}`)
        .then((res) => res.json())
}

export const addIngredient = (ingredient) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(ingredient)
    })
}

export const deleteIngredient = (id) => {
    return fetch(baseUrl + `/delete/${id}`, {
        method: "DELETE",
    })
}

export const editIngredient = (id, ingredient) => {
    return fetch(baseUrl + `/${id}`, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(ingredient)
    })

}