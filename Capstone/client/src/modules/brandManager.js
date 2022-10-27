const baseUrl = '/api/brand'

export const getAllBrands = () => {
    return fetch(baseUrl)
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