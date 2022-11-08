
import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { addIngredientHazard } from "../../modules/ingredientHazardManager"
import { addUse } from "../../modules/useManager"

export const IngredientHazardForm = ({ setHazardForm, ingredient, getIngredient, getIngredientHazards, hazards }) => {
    const navigate = useNavigate()
    const [ingredientHazard, updateIngredientHazard] = useState({
        hazardId: 0,
        ingredientId: ingredient.id,
        case: ""

    })

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        const ingredientHazardToSend = {
            hazardId: parseInt(ingredientHazard.hazardId),
            case: ingredientHazard.case,
            ingredientId: parseInt(ingredientHazard.ingredientId)

        }

        return addIngredientHazard(ingredientHazardToSend)
            .then(() => {
                setHazardForm(false);
                getIngredient();
                getIngredientHazards();
            })
    }
    return (<>
        <Card>
            <CardBody>
                <FormGroup>
                    <Label for="hazardSelect">Hazard</Label>
                    <Input
                        id="hazardSelect"
                        name="hazardId"
                        type="select"
                        onChange={
                            (evt) => {
                                const copy = { ...ingredientHazard }
                                copy.hazardId = evt.target.value
                                updateIngredientHazard(copy)
                            }
                        }>
                        <option>choose the hazard</option>
                        {hazards.map((h) => (
                            <option value={h.id} key={h.id}>{h.name}</option>
                        ))}
                    </Input>
                </FormGroup>

                <Form className="useForm">
                    <FormGroup>
                        <Label for="case">Hazard Case</Label>
                        <Input
                            id="case"
                            name="case"
                            type="textarea"
                            value={ingredientHazard.case}
                            onChange={
                                (evt) => {
                                    const copy = { ...ingredientHazard }
                                    copy.case = evt.target.value
                                    updateIngredientHazard(copy)
                                }
                            } />
                    </FormGroup>

                    <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                        className="btn btn-primary">
                        Save
                    </Button>
                    <Button onClick={() => setHazardForm(false)}>Cancel</Button>
                </Form>

            </CardBody>
        </Card>
    </>)
}