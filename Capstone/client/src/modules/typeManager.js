const baseUrl = '/api/type'

export const getAllTypes = () => {
    return fetch(baseUrl)
        .then((res) => res.json())
}