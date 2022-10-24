const baseUrl = '/api/rate'

export const getAllRates = () => {
    return fetch(baseUrl)
        .then((res) => res.json())
}