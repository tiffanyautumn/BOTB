import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button } from "reactstrap"
import { ProductIngredientDelete } from "./ProductIngredientDelete"
import { ProductIngredientEdit } from "./ProductIngredientEdit"

export const ProductIngredient = ({ productIngredient, getProduct, isAdmin, isApproved }) => {
    const navigate = useNavigate()
    const [formActive, setFormActive] = useState(false)
    const [deleteActive, setDeleteActive] = useState(false)


    return <>
        <tr>
            {
                formActive || deleteActive
                    ? ""
                    : <>
                        <td>{productIngredient.ingredient.name}</td>
                        <td>{productIngredient.use}</td>
                        <td>{productIngredient.ingredient.safetyInfo}</td>
                        <td>{productIngredient.ingredient?.ingredientReview?.rate?.rating}</td>
                        <td><Button onClick={(() => { navigate(`/ingredient/${productIngredient.ingredientId}`) })}>Details</Button></td>
                    </>
            }
            {
                isAdmin
                    ? <td><Button onClick={(() => { setDeleteActive(true) })}>Delete</Button></td>
                    : ""
            }
            {
                isApproved
                    ? <td><Button onClick={(() => { setFormActive(true) })}>Edit</Button></td>
                    : ""
            }
        </tr>
        <tr>
            {
                formActive
                    ? <td><ProductIngredientEdit ProductIngredient={productIngredient} setFormActive={setFormActive} getProduct={getProduct} /></td>
                    : ""
            }
            {
                deleteActive
                    ? <td><ProductIngredientDelete ProductIngredient={productIngredient} setDeleteActive={setDeleteActive} getProduct={getProduct} /></td>
                    : ""
            }
        </tr>
    </>
}