import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { addType, editType, getTypeById } from "../../modules/typeManager"

export const TypeEdit = ({ Type, setEditForm, getTypes }) => {
    const navigate = useNavigate()
    const [type, updateType] = useState({
        name: "",
        id: 0

    })

    const getType = () => {
        getTypeById(Type.id).then(t => updateType(t))
    }

    useEffect(
        () => {
            getType()
        }, []
    )

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        const typeToSend = {
            name: type.name,
            id: type.id

        }

        return editType(type.id, typeToSend)
            .then(() => {
                setEditForm(false);
                getTypes();
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

            </CardBody>
        </Card>
    </>)
}