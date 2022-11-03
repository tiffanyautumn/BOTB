const baseUrl = '/api/type'

export const getAllTypes = () => {
    return fetch(baseUrl)
        .then((res) => res.json())
}

export const getTypeById = (id) => {
    return fetch(baseUrl + `/${id}`)
        .then((res) => res.json())
}

export const addType = (type) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(type)
    })
}

export const deleteType = (id) => {
    return fetch(baseUrl + `/delete/${id}`, {
        method: "DELETE",
    })
}

export const editType = (id, type) => {
    return fetch(baseUrl + `/${id}`, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(type)
    })

}