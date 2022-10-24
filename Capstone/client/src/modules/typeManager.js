const baseUrl = '/api/type'

export const getAllTypes = () => {
    return fetch(baseUrl)
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