import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { addType } from "../../modules/typeManager"

export const TypeForm = () => {
    const navigate = useNavigate()
    const [type, updateType] = useState({
        name: ""

    })

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        const typeToSend = {
            name: type.name

        }

        return addType(typeToSend)
            .then(() => {
                navigate("/type")
            })
    }
    return (<>
        <Card>
            <CardBody>

                <Form className="typeForm">
                    <FormGroup>
                        <Label for="name">Type</Label>
                        <Input
                            id="name"
                            name="name"
                            type="text"
                            value={type.name}
                            onChange={
                                (evt) => {
                                    const copy = { ...type }
                                    copy.name = evt.target.value
                                    updateType(copy)
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