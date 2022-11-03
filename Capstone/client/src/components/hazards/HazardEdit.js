import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { editBrand, getBrandById } from "../../modules/brandManager"
import { editHazard, getHazardById } from "../../modules/hazardManager"

export const HazardEdit = ({ Hazard, setEditForm, getHazards }) => {
    const navigate = useNavigate()
    const [hazard, updateHazard] = useState({
        name: "",
        description: "",
        id: 0

    })

    const getHazard = () => {
        getHazardById(Hazard.id).then(b => updateHazard(b))
    }

    useEffect(
        () => {
            getHazard()
        }, []
    )

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        const hazardToSend = {
            name: hazard.name,
            description: hazard.description,
            id: hazard.id

        }

        return editHazard(hazard.id, hazardToSend)
            .then(() => {
                setEditForm(false);
                getHazards();
            })
    }

    return <>
        <Card>
            <CardBody>

                <Form className="typeForm">
                    <FormGroup>
                        <Label for="name" > Name </Label>
                        < Input
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
                        <Label for="description" > Description </Label>
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
                        className="btn btn-primary" >
                        Save
                    </Button>
                </Form>
            </CardBody>
        </Card>
    </>
}