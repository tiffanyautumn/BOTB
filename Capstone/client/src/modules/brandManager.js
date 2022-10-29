const baseUrl = '/api/brand'

export const getAllBrands = () => {
    return fetch(baseUrl)
        .then((res) => res.json())
}

export const getBrandById = (id) => {
    return fetch(baseUrl + `/${id}`)
        .then((res) => res.json())
}

export const addBrand = (brand) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(brand)
    })
}


export const deleteBrand = (id) => {
    return fetch(baseUrl + `/delete/${id}`, {
        method: "DELETE",
    })
}

export const editBrand = (id, brand) => {
    return fetch(baseUrl + `/${id}`, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(brand)
    })

}