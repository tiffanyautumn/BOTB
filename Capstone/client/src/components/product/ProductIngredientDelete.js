import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button } from "reactstrap"
import { deleteIngredientReview, getIngredientReviewById } from "../../modules/ingredientReviewManager"
import { deleteProductIngredient, getProductIngredientById } from "../../modules/productIngredientManager"

export const ProductIngredientDelete = ({ ProductIngredient, setDeleteActive, getProduct }) => {
    const [productIngredient, setProductIngredient] = useState([])
    const navigate = useNavigate()

    const getProductIngredient = () => {
        getProductIngredientById(ProductIngredient.id).then(i => setProductIngredient(i))
    }

    useEffect(
        () => {
            getProductIngredient()
        }, []
    )

    const deleteButton = () => {
        return deleteProductIngredient(productIngredient.id)
            .then(() => {
                getProduct()
                setDeleteActive(false)
            })
    }
    return <>
        <td>Are you sure that you want to delete {ProductIngredient.ingredient.name}?</td>
        <td><Button onClick={() => { deleteButton() }}>Delete</Button></td>
        <td><Button onClick={() => { setDeleteActive(false) }}>Cancel</Button></td>
    </>
}