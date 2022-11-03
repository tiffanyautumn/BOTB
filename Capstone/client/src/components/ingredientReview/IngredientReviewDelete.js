import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button } from "reactstrap"
import { deleteIngredientReview, getIngredientReviewById } from "../../modules/ingredientReviewManager"

export const IngredientReviewDelete = () => {
    const { ingredientReviewId } = useParams()
    const [ingredientReview, setIngredientReview] = useState([])
    const navigate = useNavigate()

    const getIngredientReview = () => {
        getIngredientReviewById(ingredientReviewId).then(i => setIngredientReview(i))
    }

    useEffect(
        () => {
            getIngredientReview()
        }, []
    )

    const deleteButton = () => {
        return deleteIngredientReview(ingredientReview.id)
            .then(() => {
                navigate(`/ingredient/${ingredientReview.ingredientId}`)
            })
    }
    return <>
        <h2>Are you sure that you want to delete the review of {ingredientReview?.ingredient?.name}</h2>
        <Button onClick={() => { deleteButton() }}>Delete</Button>
        <Button onClick={() => { navigate(`/ingredient/${ingredientReview.ingredientId}`) }}>Cancel</Button>
    </>
}