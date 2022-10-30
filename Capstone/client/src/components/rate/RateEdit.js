import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { editRate, getRateById } from "../../modules/rateManager"

export const RateEdit = ({ Rate, setEditForm, getRates }) => {
    const navigate = useNavigate()
    const [rate, updateRate] = useState({
        rating: "",
        id: 0

    })

    const getRate = () => {
        getRateById(Rate.id).then(r => updateRate(r))
    }

    useEffect(
        () => {
            getRate()
        }, []
    )

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        const rateToSend = {
            rating: rate.rating,
            id: rate.id

        }

        return editRate(rate.id, rateToSend)
            .then(() => {
                setEditForm(false);
                getRates();
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