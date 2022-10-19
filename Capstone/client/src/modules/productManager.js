const baseUrl = '/api/product'

export const getAllProducts = () => {
    return fetch(baseUrl)
        .then((res) => res.json())
}

export const getProductById = (id) => {
    return fetch(`/api/product/${id}`)
        .then((res) => res.json())
}

export const addProduct = (product) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(product)
    })
}

export const deleteProduct = (id) => {
    return fetch(baseUrl + `/delete/${id}`, {
        method: "DELETE",
    })
}