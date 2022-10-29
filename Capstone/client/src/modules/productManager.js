import { getToken } from "./authManager";
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

export const editProduct = (id, product) => {
    return fetch(baseUrl + `/${id}`, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(product)
    })

}

export const getAllUserProducts = () => {
    return getToken().then((token) => {
        return fetch(baseUrl + `/getUserProducts`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((res) => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get your products.",
                );
            }
        })
    })
}

export const addUserProduct = (product) => {
    return getToken().then((token) => {
        return fetch(baseUrl + `/create/userProduct`, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(product)
        })
    })
}


export const deleteUserProduct = (id) => {
    return fetch(baseUrl + `/delete/userProduct/${id}`, {
        method: "DELETE",
    })
}