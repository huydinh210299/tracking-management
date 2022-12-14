import {
  Table,
  Input,
  Button,
  Space,
  Select,
  Drawer,
  Form,
  notification,
  Checkbox,
  DatePicker,
  Card,
} from "antd"
import { CheckOutlined, CloseCircleOutlined } from "@ant-design/icons"
import { useEffect, useState } from "react"
import moment from "moment"

import makeRequest from "../../utils/makeRequest"
import { RECORD_MODE } from "../../const/mode"
import { addListKey } from "../../utils/addListKey"
import omitNil from "../../utils/omit"
import { deviceConfig } from "../../config/device"
import SearchText from "../seachItem/SearchText"
import { requestUrl } from "../../resource/requestUrl"
import CustomSkeleton from "../skeleton/CustomSkeleton"

const { Option } = Select

const DeviceManager = () => {
  const [devices, setDevices] = useState([])
  const [userUnits, setUserUnits] = useState([])
  const [cars, setCars] = useState([])
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 10,
    total: 0,
    showTotal: (total, range) => `${range[0]}-${range[1]} of ${total}`,
  })
  const [filter, setFilter] = useState({
    deviceNumber: "",
    phone: "",
    unitId: null,
    imei: "",
    mobileCarrier: null,
  })
  const [lastFilter, setLastFilter] = useState({
    deviceNumber: "",
    phone: "",
    unitId: null,
    imei: "",
    mobileCarrier: null,
  })
  const [selectedRowArr, setSelectedRowArr] = useState([])
  const [visible, setVisible] = useState(false)
  const [mode, setMode] = useState(RECORD_MODE.CREATE)
  const [selectedRecord, setSelectedRecord] = useState({})
  const [loading, setLoading] = useState(true)

  const handleSearch = async (params, pagination = {}) => {
    setLoading(true)
    let filter = omitNil(params)
    makeRequest({
      method: "GET",
      url: requestUrl.device.readUrl(),
      params: {
        ...filter,
        page: pagination.current,
        record: pagination.pageSize,
      },
    }).then((searchRs) => {
      setDevices(addListKey(searchRs.data))
      setPagination({
        ...pagination,
        total: searchRs.totalRecords,
      })
      setLoading(false)
    })
  }

  const handlePaginationChange = (page, pageSize) => {
    let newPagination = { ...pagination, current: page, pageSize }
    handleSearch(filter, newPagination)
  }

  const columns = [
    {
      title: "M?? thi???t b???",
      dataIndex: "deviceNumber",
      width: 200,
      ellipsis: true,
    },
    {
      title: "S??? ??i???n tho???t",
      dataIndex: "phone",
      width: 200,
    },
    {
      title: "Nh?? m???ng",
      render: (text, record) => {
        return (
          <>
            {record.mobileCarrier
              ? deviceConfig.mobileCarrierObj[record.mobileCarrier]
              : ""}
          </>
        )
      },
      width: 200,
    },
    {
      title: "Xe",
      render: (text, record) => (
        <>{record.car ? record.car.licensePlate : ""}</>
      ),
      width: 200,
    },
    {
      title: "????n v???",
      render: (text, record) => {
        return <>{record.unit ? record.unit.name : ""}</>
      },
    },
    {
      title: "IMEI Sim",
      dataIndex: "imei",
    },
    {
      title: "K??ch ho???t",
      dataIndex: "status",
      render: (text, record) => <Checkbox checked={record.status} disabled />,
      width: 100,
      align: "center",
    },
    {
      title: "C???p nh???t",
      dataIndex: "allowUpdate",
      render: (text, record) => (
        <Checkbox checked={record.allowUpdate} disabled />
      ),
      width: 100,
      align: "center",
    },
    {
      title: "Xo??",
      render: (text, record) => (
        <CloseCircleOutlined style={{ color: "red" }} />
      ),
      align: "center",
      width: 50,
    },
  ]

  // Function handle selectedRow
  const onRowSelected = (record, selected, selectedRows) => {
    setVisible(true)
    let selectedRecord = selectedRows[0]
    let formatedRecord = {
      ...selectedRecord,
      activationTime: selectedRecord.activationTime
        ? moment(selectedRecord.activationTime)
        : null,
    }
    setSelectedRecord(formatedRecord)
    form.resetFields()
    form.setFieldsValue(formatedRecord)
    setMode(RECORD_MODE.UPDATE)
  }

  const onSelectedChange = (selectedRowKeys, selectedRows) => {
    setSelectedRowArr(selectedRowKeys)
  }

  // Function handle drawer
  const onClose = () => {
    setSelectedRowArr([])
    setSelectedRecord({})
    setVisible(false)
  }

  useEffect(() => {
    const getData = async () => {
      let [deviceRs, userUnitRs, carRs] = await Promise.all([
        makeRequest({
          method: "GET",
          url: requestUrl.device.readUrl(),
        }),
        makeRequest({
          method: "GET",
          url: requestUrl.userUnit.readUrl(),
        }),
        makeRequest({
          method: "GET",
          url: requestUrl.car.readUrl(),
          params: {
            paging: false,
          },
        }),
      ])

      setDevices(addListKey(deviceRs.data))
      setCars(addListKey(carRs.data))
      setUserUnits(addListKey(userUnitRs.data))
      setPagination({ ...pagination, total: deviceRs.totalRecords })
      setLoading(false)
    }

    getData()
  }, [])

  const [form] = Form.useForm()

  const onFinish = (device) => {
    setLoading(true)
    let deviceData = {
      ...device,
      activationTime: moment(device.activationTime).format("YYYY/MM/DD"),
    }
    let method = mode === RECORD_MODE.CREATE ? "POST" : "PUT"
    let url =
      mode === RECORD_MODE.CREATE
        ? requestUrl.device.createUrl()
        : requestUrl.device.updateUrl({ id: device.id })
    let msg =
      mode === RECORD_MODE.CREATE
        ? "Th??m m???i thi???t b??? th??nh c??ng"
        : "C???p nh???t th??nh c??ng"
    makeRequest({
      method,
      url,
      data: deviceData,
    }).then((rs) => {
      notification.open({
        message: "Th??ng b??o",
        icon: <CheckOutlined style={{ color: "#2fd351" }} />,
        description: msg,
      })
      handleSearch(filter, pagination)
      setSelectedRowArr([])
      setSelectedRecord({})
      setVisible(false)
      setLoading(false)
    })
  }

  const openCreateForm = () => {
    setVisible(true)
    setMode(RECORD_MODE.CREATE)
    setSelectedRecord({})
    form.resetFields()
    form.setFieldsValue({})
  }

  return (
    <>
      <Card
        title="Danh s??ch thi???t b???"
        extra={
          <Button type="primary" onClick={openCreateForm}>
            Th??m thi???t b??? m???i
          </Button>
        }
        style={{ height: "100%" }}
      >
        <Space style={{ marginBottom: 10 }}>
          <SearchText
            dataIndex="deviceNumber"
            filter={filter}
            pagination={pagination}
            handleSearch={handleSearch}
            placeholder={"M?? thi???t b???"}
            setFilter={setFilter}
            setLastFilter={setLastFilter}
            lastFilter={lastFilter}
          ></SearchText>
          <SearchText
            dataIndex="phone"
            filter={filter}
            pagination={pagination}
            handleSearch={handleSearch}
            placeholder={"S??? ??i???n tho???i"}
            setFilter={setFilter}
            setLastFilter={setLastFilter}
            lastFilter={lastFilter}
          ></SearchText>
          <SearchText
            dataIndex="IMEI"
            filter={filter}
            pagination={pagination}
            handleSearch={handleSearch}
            placeholder={"IMEI"}
            setFilter={setFilter}
            setLastFilter={setLastFilter}
            lastFilter={lastFilter}
          ></SearchText>
        </Space>
        <Table
          dataSource={devices}
          columns={columns}
          pagination={{
            position: ["bottomRight"],
            ...pagination,
            onChange: handlePaginationChange,
          }}
          size="small"
          rowSelection={{
            selectedRowKeys: selectedRowArr,
            columnTitle: <div>S???a</div>,
            columnWidth: 50,
            type: "checkbox",
            onSelect: (record, selected, selectedRows) =>
              onRowSelected(record, selected, selectedRows),
            onChange: (selectedRowKeys, selectedRows) =>
              onSelectedChange(selectedRowKeys, selectedRows),
          }}
        />
      </Card>

      <Drawer
        title={mode === RECORD_MODE.CREATE ? "Th??m thi???t b??? m???i" : "C???p nh???t"}
        placement="right"
        onClose={onClose}
        visible={visible}
        width="450"
      >
        <Form
          name="rfidForm"
          labelCol={{ span: 8 }}
          wrapperCol={{ span: 16 }}
          initialValues={{
            ...selectedRecord,
            activationTime: selectedRecord.activationTime
              ? moment(selectedRecord.activationTime)
              : "",
          }}
          form={form}
          labelAlign="left"
          onFinish={onFinish}
        >
          <Form.Item label="id" name="id" noStyle>
            <Input type="hidden" />
          </Form.Item>
          <Form.Item
            label="IMEI thi???t b???"
            name="imei"
            rules={[
              {
                required: true,
                message: "Tr?????ng n??y kh??ng ???????c thi???u",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="M?? thi???t b???"
            name="deviceNumber"
            rules={[
              {
                required: true,
                message: "Tr?????ng n??y kh??ng ???????c thi???u",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="S??? ??i???n tho???i"
            name="phone"
            rules={[
              {
                required: true,
                message: "Tr?????ng n??y kh??ng ???????c thi???u",
              },
            ]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            name="unitId"
            label="????n v??? qu???n l??"
            rules={[
              {
                required: true,
                message: "Tr?????ng n??y kh??ng ???????c thi???u",
              },
            ]}
          >
            <Select>
              {userUnits.map((item, index) => (
                <Option value={item.id} key={index}>
                  {item.name}
                </Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item
            name="mobileCarrier"
            label="Nh?? m???ng"
            rules={[
              {
                required: true,
                message: "Tr?????ng n??y kh??ng ???????c thi???u",
              },
            ]}
          >
            <Select>
              {deviceConfig.mobileCarrier.map((item, index) => (
                <Option value={item.value} key={index}>
                  {item.label}
                </Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item
            name="carId"
            label="Xe mang bi???n s???"
            rules={[
              {
                required: true,
                message: "Tr?????ng n??y kh??ng ???????c thi???u",
              },
            ]}
          >
            <Select>
              {cars.map((item, index) => (
                <Option value={item.id} key={index}>
                  {item.licensePlate}
                </Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item name="activationTime" label="Ng??y k??ch ho???t">
            <DatePicker format="DD/MM/YYYY" />
          </Form.Item>

          <Form.Item
            name="status"
            label="Th??? ??ang k??ch ho???t"
            valuePropName="checked"
          >
            <Checkbox />
          </Form.Item>

          <Form.Item
            name="allowUpdate"
            label="Cho ph??p c???p nh???t"
            valuePropName="checked"
          >
            <Checkbox />
          </Form.Item>

          <Form.Item
            wrapperCol={{
              offset: 0,
              span: 24,
            }}
          >
            <Button type="primary" htmlType="submit">
              L??u
            </Button>
          </Form.Item>
        </Form>
      </Drawer>
      <CustomSkeleton loading={loading} />
    </>
  )
}

export default DeviceManager
