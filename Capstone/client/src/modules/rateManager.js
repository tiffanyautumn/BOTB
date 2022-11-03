const baseUrl = '/api/rate'

export const getAllRates = () => {
    return fetch(baseUrl)
        .then((res) => res.json())
}

export const getRateById = (id) => {
    return fetch(baseUrl + `/${id}`)
        .then((res) => res.json())
}

export const addRate = (rate) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(rate)
    })
}

export const deleteRate = (id) => {
    return fetch(baseUrl + `/delete/${id}`, {
        method: "DELETE",
    })
}

export const editRate = (id, rate) => {
    return fetch(baseUrl + `/${id}`, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(rate)
    })

}