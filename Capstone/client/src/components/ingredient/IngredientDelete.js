import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button } from "reactstrap"
import { deleteIngredient, getIngredientById } from "../../modules/ingredientManager"

export const IngredientDelete = () => {
    const { ingredientId } = useParams()
    const [ingredient, setIngredient] = useState([])
    const navigate = useNavigate()

    const getIngredient = () => {
        getIngredientById(ingredientId).then(i => setIngredient(i))
    }

    useEffect(
        () => {
            getIngredient()
        }, []
    )

    const deleteButton = () => {
        return deleteIngredient(ingredient.id)
            .then(() => {
                navigate("/ingredient")
            })
    }
    return <>
        <h2>Are you sure that you want to delete {ingredient.name}</h2>
        <Button onClick={() => { deleteButton() }}>Delete</Button>
        <Button onClick={() => { navigate(`/ingredient/${ingredientId}`) }}>Cancel</Button>
    </>
}