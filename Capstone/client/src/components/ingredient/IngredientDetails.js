import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button, Card, CardBody, CardImg, Col, Table } from "reactstrap"
import { getIngredientById } from "../../modules/ingredientManager"

export const IngredientDetails = ({ isAdmin, isApproved }) => {
    const { ingredientId } = useParams()
    const navigate = useNavigate()
    const [ingredient, setIngredient] = useState([])

    const getIngredient = () => {
        getIngredientById(ingredientId).then(i => setIngredient(i))
    }

    useEffect(
        () => {
            getIngredient()
        }, []
    )

    const ingredientReviewDisplay = () => {
        return <>
            <Card key={ingredient?.ingredientReview?.Id} style={{ width: '50%' }}>
                <CardBody>
                    <p>Our Review</p>
                    <p>{ingredient?.ingredientReview?.rate?.rating}</p>
                    <p>{ingredient?.ingredientReview?.review}</p>
                    <p>{ingredient?.ingredientReview?.source}</p>
                    <p>{ingredient?.ingredientReview?.userProfile?.firstName} {ingredient?.ingredientReview?.userProfile?.lastName}</p>
                    <p>{ingredient?.ingredientReview?.dateReviewed}</p>
                    {
                        isApproved
                            ? <Button onClick={(() => navigate(`/ingredientReview/edit/${ingredient.ingredientReview.id}`))}>Edit Review</Button>
                            : ""
                    }
                    {
                        isAdmin
                            ? <Button onClick={(() => navigate(`/ingredientReview/delete/${ingredient.ingredientReview.id}`))}>Delete Review</Button>
                            : ""
                    }


                </CardBody>
            </Card>
        </>
    }
    return <>
        <div className="row justify-content-center">
            <Card key={ingredient.id} style={{
                width: '50%'
            }}
            >
                <Col>
                    <CardBody>

                        <p>{ingredient?.name}</p>
                        <p>function: {ingredient?.function}</p>
                        <p>safety information: {ingredient?.safetyInfo}</p>
                        {
                            isApproved
                                ? <Button onClick={(() => navigate(`/ingredient/edit/${ingredient.id}`))}>edit</Button>
                                : ""
                        }
                        {
                            isAdmin
                                ? <Button onClick={(() => navigate(`/ingredient/delete/${ingredient.id}`))}>delete</Button>
                                : ""
                        }


                    </CardBody>
                </Col>
            </Card >
            {
                ingredient.ingredientReview
                    ? ingredientReviewDisplay()
                    : ""
            }
            {
                isApproved && !ingredient.ingredientReview
                    ? <Button onClick={(() => navigate(`/ingredientReview/create/${ingredientId}`))}>Add an Ingredient Review </Button>
                    : ""
            }
        </div>

    </>
}