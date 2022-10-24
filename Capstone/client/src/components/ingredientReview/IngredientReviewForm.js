import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { addIngredientReview } from "../../modules/ingredientReviewManager"
import { getAllRates } from "../../modules/roleManager"
import { getUserProfiles } from "../../modules/userProfileManager"

export const IngredientReviewForm = () => {
    const navigate = useNavigate()
    const [rates, setRates] = useState([])
    const [users, setUsers] = useState([])
    const { ingredientId } = useParams()
    const [ingredientReview, updateIngredientReview] = useState({
        rateId: 0,
        review: "",
        source: "",
        userId: 0
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
            source: ingredientReview.source,
            userId: parseInt(ingredientReview.userId)
        }

        return addIngredientReview(ingredientReviewToSend)
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
                        <Label for="source">Source</Label>
                        <Input
                            id="source"
                            name="source"
                            type="text"
                            value={ingredientReview.source}
                            onChange={
                                (evt) => {
                                    const copy = { ...ingredientReview }
                                    copy.source = evt.target.value
                                    updateIngredientReview(copy)
                                }
                            } />
                    </FormGroup>
                    {/* <FormGroup>
                        <Label for="dateReviewed">Date Reviewed</Label>
                        <Input
                            id="dateReviewed"
                            name="dateReviewed"
                            type="date"
                            value={ingredientReview.dateReviewed}
                            onChange={
                                (evt) => {
                                    const copy = { ...ingredientReview }
                                    copy.dateReviewed = evt.target.value
                                    updateIngredientReview(copy)
                                }
                            } />
                    </FormGroup> */}
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
                        <Label for="userSelect">Reviewer</Label>
                        <Input
                            id="userSelect"
                            name="userId"
                            type="select"
                            onChange={
                                (evt) => {
                                    const copy = { ...ingredientReview }
                                    copy.userId = evt.target.value
                                    updateIngredientReview(copy)
                                }
                            }>
                            <option>Choose a reviewer</option>
                            {users.map((u) => (
                                <option value={u.id} key={u.id}>{u.firstName} {u.lastName}</option>
                            ))}
                        </Input>
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