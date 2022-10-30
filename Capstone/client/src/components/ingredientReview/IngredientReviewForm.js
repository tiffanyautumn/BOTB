import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { addIngredientReview } from "../../modules/ingredientReviewManager"
import { getAllRates } from "../../modules/roleManager"
import { addSource } from "../../modules/sourceManager"
import { getUserProfiles } from "../../modules/userProfileManager"

export const IngredientReviewForm = () => {
    const navigate = useNavigate()
    const [rates, setRates] = useState([])
    const [users, setUsers] = useState([])
    const { ingredientId } = useParams()
    const [ingredientReview, updateIngredientReview] = useState({
        rateId: 0,
        review: "",
        link: ""
    })


    const getRates = () => {
        getAllRates().then(r => setRates(r))
    }

    const getUsers = () => {
        getUserProfiles().then(up => setUsers(up))
    }

    useEffect(() => {
        getRates();
        getUsers();

    }, []);

    const handleSaveButtonClick = (event) => {
        event.preventDefault()


        const ingredientReviewToSend = {
            rateId: parseInt(ingredientReview.rateId),
            ingredientId: parseInt(ingredientId),
            review: ingredientReview.review,
        }
        const sourceToSend = {
            link: ingredientReview.link
        }

        return addIngredientReview(ingredientReviewToSend)
            .then(res => res.json())
            .then((createdReview) => {
                sourceToSend.ingredientReviewId = createdReview.id;
                addSource(sourceToSend);
            })
            .then(() => {
                navigate(`/ingredient/${ingredientId}`)
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
                            type="textarea"
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
                            <option>Choose a rating</option>
                            {rates.map((r) => (
                                <option value={r.id} key={r.id}>{r.rating}</option>
                            ))}
                        </Input>
                    </FormGroup>

                    <FormGroup>
                        <Label for="link">Source</Label>
                        <Input
                            id="link"
                            name="link"
                            type="text"
                            value={ingredientReview.link}
                            onChange={
                                (evt) => {
                                    const copy = { ...ingredientReview }
                                    copy.link = evt.target.value
                                    updateIngredientReview(copy)
                                }
                            } />
                    </FormGroup>

                    <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                        className="btn btn-primary">
                        Save
                    </Button>
                </Form>
                <Button onClick={() => navigate(`/ingredient/${ingredientId}`)}>Cancel</Button>
            </CardBody>
        </Card>
    </>)
}