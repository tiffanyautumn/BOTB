
import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { addPIUse, addUse, getUsesByIngredientId } from "../../modules/useManager"

export const PIUseForm = ({ setUseForm, ProductIngredient, getPIs }) => {
    const navigate = useNavigate()
    const [uses, setUses] = useState([])
    const [PIuse, updatePIUse] = useState({
        useId: 0,
        productIngredientId: ProductIngredient?.id

    })

    const getPIUses = () => {
        getUsesByIngredientId(ProductIngredient?.ingredientId).then(u => setUses(u))
    }

    useEffect(
        () => {
            getPIUses();
        }, []
    )
    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        const useToSend = {
            productIngredientId: parseInt(PIuse.productIngredientId),
            useId: parseInt(PIuse.useId)

        }

        return addPIUse(useToSend)
            .then(() => {
                setUseForm(false);
                getPIs();
            })
    }
    return (<>
        <Card>
            <CardBody>

                <Form className="useForm">
                    <FormGroup>
                        <Label for="useSelect">Use</Label>
                        <Input
                            id="useSelect"
                            name="useId"
                            type="select"
                            onChange={
                                (evt) => {
                                    const copy = { ...PIuse }
                                    copy.useId = evt.target.value
                                    updatePIUse(copy)
                                }
                            }>
                            <option>choose the use</option>
                            {uses.map((u) => (
                                <option value={u.id} key={u.id}>{u.description}</option>
                            ))}
                        </Input>
                    </FormGroup>

                    <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                        className="btn btn-primary">
                        Save
                    </Button>
                    <Button onClick={() => setUseForm(false)}>Cancel</Button>
                </Form>

            </CardBody>
        </Card>
    </>)
}