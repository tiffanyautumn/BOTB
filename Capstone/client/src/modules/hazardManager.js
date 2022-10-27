const baseUrl = '/api/hazard'

export const getAllHazards = () => {
    return fetch(baseUrl)
        .then((res) => res.json())
}

export const getHazardById = (id) => {
    return fetch(`/api/hazard/${id}`)
        .then((res) => res.json())
}

export const addHazard = (hazard) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(hazard)
    })
}

export const deleteHazard = (id) => {
    return fetch(baseUrl + `/delete/${id}`, {
        method: "DELETE",
    })
}

export const editHazard = (id, hazard) => {
    return fetch(baseUrl + `/${id}`, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(hazard)
    })

}