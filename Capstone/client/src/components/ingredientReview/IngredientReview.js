import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button } from "reactstrap"
import { getSourcesByReviewId } from "../../modules/sourceManager"

export const IngredientReview = ({ IngredientReview, isAdmin, isApproved }) => {
    const [sources, setSources] = useState([])
    const navigate = useNavigate()
    const getSources = () => {
        getSourcesByReviewId(IngredientReview.id).then((s) => setSources(s))
    }

    useEffect(
        () => {
            getSources();
        }, []
    )
    return <>
        <p>{IngredientReview?.rate?.rating}</p>
        <p>{IngredientReview?.review}</p>
        <p>{IngredientReview?.userProfile?.firstName} {IngredientReview?.userProfile?.lastName}</p>
        {sources.map((s) => { return <p>Source: {s.link}</p> })}
        <p>Date Reviewed: {new Date(IngredientReview?.dateReviewed).toLocaleDateString('en-US')}</p>
        {
            isApproved
                ? <Button onClick={(() => navigate(`/ingredientReview/edit/${IngredientReview.id}`))}>Edit Review</Button>
                : ""
        }
        {
            isAdmin
                ? <Button onClick={(() => navigate(`/ingredientReview/delete/${IngredientReview.id}`))}>Delete Review</Button>
                : ""
        }
    </>
}