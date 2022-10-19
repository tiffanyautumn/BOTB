import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button } from "reactstrap"
import { deleteProduct, getProductById } from "../../modules/productManager"

export const ProductDelete = () => {
    const { productId } = useParams()
    const [product, setProduct] = useState([])
    const navigate = useNavigate()

    const getProduct = () => {
        getProductById(productId).then(p => setProduct(p))
    }

    useEffect(
        () => {
            getProduct()
        }, []
    )

    const deleteButton = () => {
        return deleteProduct(product.id)
            .then(() => {
                navigate("/product")
            })
    }
    return <>
        <h2>Are you sure that you want to delete {product.name}</h2>
        <Button onClick={() => { deleteButton() }}>Delete</Button>
        <Button onClick={() => { navigate("/product") }}>Cancel</Button>
    </>
}