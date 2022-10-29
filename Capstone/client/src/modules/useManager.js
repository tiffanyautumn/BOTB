const baseUrl = '/api/use'



export const addUse = (use) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(use)
    })
}

export const addPIUse = (use) => {
    return fetch(baseUrl + `/productIngredient`, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(use)
    })
}

export const getUsesByIngredientId = (id) => {
    return fetch(`/api/use/getbyingredient/${id}`)
        .then((res) => res.json())
}

export const deleteUse = (id) => {
    return fetch(baseUrl + `/delete/${id}`, {
        method: "DELETE",
    })
}