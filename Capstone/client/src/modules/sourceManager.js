const baseUrl = '/api/source'


export const getAllTypes = () => {
    return fetch(baseUrl)
        .then((res) => res.json())
}

export const getSourcesByReviewId = (id) => {
    return fetch(baseUrl + `/review/${id}`)
        .then((res) => res.json())
}
export const getSourceById = (id) => {
    return fetch(baseUrl + `/${id}`)
        .then((res) => res.json())
}

export const addSource = (source) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(source)
    })
}

export const deleteSource = (id) => {
    return fetch(baseUrl + `/delete/${id}`, {
        method: "DELETE",
    })
}

export const editSource = (id, source) => {
    return fetch(baseUrl + `/${id}`, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(source)
    })

}