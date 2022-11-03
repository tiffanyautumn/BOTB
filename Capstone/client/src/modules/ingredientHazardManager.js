const baseUrl = '/api/ingredientHazard'



export const getIngredientHazardById = (id) => {
    return fetch(`/api/ingredientHazard/${id}`)
        .then((res) => res.json())
}

export const getIngredientHazardByIngredientId = (id) => {
    return fetch(baseUrl + `/ingredient/${id}`)
        .then((res) => res.json())
}

export const addIngredientHazard = (ingredientHazard) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(ingredientHazard)
    })
}

export const deleteIngredientHazard = (id) => {
    return fetch(baseUrl + `/delete/${id}`, {
        method: "DELETE",
    })
}

export const editIngredientHazard = (id, ingredientHazard) => {
    return fetch(baseUrl + `/${id}`, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(ingredientHazard)
    })

}