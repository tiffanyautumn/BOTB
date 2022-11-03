import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { editIngredientReview, getIngredientReviewById } from "../../modules/ingredientReviewManager"
import { getAllRates } from "../../modules/roleManager"
import { getUserProfiles } from "../../modules/userProfileManager"

export const IngredientReviewEdit = () => {
    const navigate = useNavigate()
    const { ingredientReviewId } = useParams()
    const [rates, setRates] = useState([])
    const [users, setUsers] = useState([])
    const [ingredientReview, updateIngredientReview] = useState({
        rateId: 0,
        review: "",
        id: 0,
        ingredientId: 0
    })

    const getIngredientReview = () => {
        getIngredientReviewById(ingredientReviewId).then(i => updateIngredientReview(i))
    }

    useEffect(
        () => {
            getIngredientReview()
            getRates();
            getUsers();
        }, []
    )

    const getRates = () => {
        getAllRates().then(r => setRates(r))
    }

    const getUsers = () => {
        getUserProfiles().then(up => setUsers(up))
    }


    const handleSaveButtonClick = (event) => {
        event.preventDefault()



        const ingredientReviewToSend = {
            rateId: parseInt(ingredientReview.rateId),
            ingredientId: parseInt(ingredientReview.ingredientId),
            review: ingredientReview.review,
            id: parseInt(ingredientReview.id)
        }


        return editIngredientReview(ingredientReviewId, ingredientReviewToSend)
            .then(() => {
                navigate(`/ingredient/${ingredientReview.ingredientId}`)
            })
    }
    return (<>
        <Card>
            <CardBody>

                <Form className="ingredientForm">
                    <FormGroup>
                        <Label for="review">Ingredient Review</Label>
                        <Input
                            id="review"
                            name="review"
                            type="text"
                            value={ingredientReview.review}
                            onChange={
                                (evt) => {
                                    const copy = { ...ingredientReview }
                                    copy.review = evt.target.value
                                    updateIngredientReview(copy)
                                }
                            } />
                    </FormGroup>

                    <FormGroup>
                        <Label for="rateSelect">Rating</Label>
                        <Input
                            id="rateSelect"
                            name="rate"
                            type="select"
                            onChange={
                                (evt) => {
                                    const copy = { ...ingredientReview }
                                    copy.rateId = evt.target.value
                                    updateIngredientReview(copy)
                                }
                            }>

                            {rates.map((r) => (
                                <option value={r.id} key={r.id}>{r.rating}</option>
                            ))}
                        </Input>
                    </FormGroup>



                    <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                        className="btn btn-primary">
                        Save
                    </Button>
                    <Button onClick={() => navigate(`/ingredient/${ingredientReview.ingredientId}`)}>Cancel</Button>
                </Form>

            </CardBody>
        </Card>
    </>)
}