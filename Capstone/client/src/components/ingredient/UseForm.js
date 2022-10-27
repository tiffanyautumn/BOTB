
import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { addUse } from "../../modules/useManager"

export const UseForm = ({ setUseForm, ingredient, getIngredient, getUses }) => {
    const navigate = useNavigate()
    const [use, updateUse] = useState({
        description: "",
        ingredientId: ingredient.id

    })

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        const useToSend = {
            description: use.description,
            ingredientId: parseInt(use.ingredientId)

        }

        return addUse(useToSend)
            .then(() => {
                setUseForm(false);
                getIngredient();
                getUses();
            })
    }
    return (<>
        <Card>
            <CardBody>

                <Form className="useForm">
                    <FormGroup>
                        <Label for="description">Use Description</Label>
                        <Input
                            id="description"
                            name="description"
                            type="text"
                            value={use.description}
                            onChange={
                                (evt) => {
                                    const copy = { ...use }
                                    copy.description = evt.target.value
                                    updateUse(copy)
                                }
                            } />
                    </FormGroup>

                    <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                        className="btn btn-primary">
                        Save
                    </Button>
                </Form>
                <Button onClick={() => setUseForm(false)}>Cancel</Button>
            </CardBody>
        </Card>
    </>)
}