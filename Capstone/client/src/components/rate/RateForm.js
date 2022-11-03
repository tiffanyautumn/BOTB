import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { addRate } from "../../modules/rateManager"
import { addType } from "../../modules/typeManager"

export const RateForm = () => {
    const navigate = useNavigate()
    const [rate, updateRate] = useState({
        rating: ""

    })

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        const rateToSend = {
            rating: rate.rating

        }

        return addRate(rateToSend)
            .then(() => {
                navigate("/rate")
            })
    }
    return (<>
        <Card>
            <CardBody>

                <Form className="rateForm">
                    <FormGroup>
                        <Label for="rating">Rate</Label>
                        <Input
                            id="rating"
                            name="rating"
                            type="text"
                            value={rate.rating}
                            onChange={
                                (evt) => {
                                    const copy = { ...rate }
                                    copy.rating = evt.target.value
                                    updateRate(copy)
                                }
                            } />
                    </FormGroup>

                    <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                        className="btn btn-primary">
                        Save
                    </Button>
                </Form>
                <Button onClick={() => navigate("/rate")}>Cancel</Button>
            </CardBody>
        </Card>
    </>)
}