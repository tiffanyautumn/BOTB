import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { addHazard } from "../../modules/hazardManager"
import { addType } from "../../modules/typeManager"

export const HazardForm = () => {
    const navigate = useNavigate()
    const [hazard, updateHazard] = useState({
        name: "",
        description: ""
    })

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        const hazardToSend = {
            name: hazard.name,
            description: hazard.description

        }

        return addHazard(hazardToSend)
            .then(() => {
                navigate("/type")
            })
    }
    return (<>
        <Card>
            <CardBody>

                <Form className="typeForm">
                    <FormGroup>
                        <Label for="name">Name</Label>
                        <Input
                            id="name"
                            name="name"
                            type="text"
                            value={hazard.name}
                            onChange={
                                (evt) => {
                                    const copy = { ...hazard }
                                    copy.name = evt.target.value
                                    updateHazard(copy)
                                }
                            } />
                    </FormGroup>

                    <FormGroup>
                        <Label for="description">Description</Label>
                        <Input
                            id="description"
                            name="description"
                            type="text"
                            value={hazard.description}
                            onChange={
                                (evt) => {
                                    const copy = { ...hazard }
                                    copy.description = evt.target.value
                                    updateHazard(copy)
                                }
                            } />
                    </FormGroup>

                    <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                        className="btn btn-primary">
                        Save
                    </Button>
                </Form>
                <Button onClick={() => navigate("/type")}>Cancel</Button>
            </CardBody>
        </Card>
    </>)
}